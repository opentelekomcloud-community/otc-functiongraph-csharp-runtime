import requests
import json
import os
from requests.exceptions import HTTPError
import yaml


conf= yaml.safe_load(open("./deployed_resources.yaml"))

print(conf["FUNCTION_ARN"])
print(conf["OBS_INPUT_BUCKET"])
print(conf["OBS_OUTPUT_BUCKET"])


iam_url = os.getenv("OTC_IAM_ENDPOINT", "https://iam.eu-de.otc.t-systems.com/v3")
domain = os.getenv("OTC_DOMAIN_NAME")  # tenant name
region = os.getenv("OTC_SDK_REGION", "eu-de")
project_id = os.getenv("OTC_SDK_PROJECTID")

username = os.getenv("OTC_USER_NAME")
password = os.getenv("OTC_USER_PASSWORD")

fg_base_url = (
    f"https://functiongraph.{region}.otc.t-systems.com/v2/{project_id}/fgs/functions"
)


#################################################################################
def token_iam(
    _username=username,
    _password=password,
    _domain=domain,
    _project_id=project_id,
    _https_insecure=False,
):
    """authenticate a user by password"""
    _auth = {
        "auth": {
            "identity": {
                "methods": ["password"],
                "password": {
                    "user": {
                        "name": _username,
                        "password": _password,
                        "domain": {"name": _domain},
                    }
                },
            },
            "scope": {"domain": {"name": _domain}, "project": {"id": _project_id}},
        }
    }

    _session = requests.session()
    _auth_str = json.dumps(_auth, indent=4)

    if "X-Auth-Token" in _session.headers:
        del _session.headers["X-Auth-Token"]

    if _https_insecure:
        _response = _session.post(
            f"{iam_url}/auth/tokens",
            data=_auth_str,
            verify=False,
            headers={"Content-Type": "application/json;charset=utf8"},
        )
    else:
        _response = _session.post(
            f"{iam_url}/auth/tokens",
            data=_auth_str,
            headers={"Content-Type": "application/json;charset=utf8"},
        )

    print("IAM authentication response code:", _response.status_code)

    if _response.status_code != 201:
        raise HTTPError(response=_response)

    _token = _response.headers["x-subject-token"]

    return _token


####################################################################
# Main
####################################################################
if __name__ == "__main__":
    token = token_iam()

    headers = {
        "x-auth-token": token,
        "Content-Type": "application/json;charset=utf8",
    }

    body = {"message": "download file"}

    function_urn = f"{conf['FUNCTION_ARN']}:latest"
    _url = f"{fg_base_url}/{function_urn}/invocations"

    r = requests.request("POST", _url, headers=headers, data=json.dumps(body))

    print("Response code:", r.status_code)
    print("Response body:", r.text)

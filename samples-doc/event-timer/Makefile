HANDLER_NAME="sample::src.Program::myFunc"


clean:
	rm -f *.zip
	rm -rf bin obj

build:
	dotnet build

all: build

initTerraform:
	terraform -chdir=terraform \
	  init \
	  -backend-config="endpoints={s3=\"https://obs.eu-de.otc.t-systems.com\"}" \
	  -backend-config="bucket=sample-tf-backend" \
	  -backend-config="key=terraform_state/csharp/doc-sample-event-timer.tf" \
	  -backend-config="region=eu-de"

deploy: all
	if [ ! -f "terraform/.terraform.lock.hcl" ]; then \
		$(MAKE) initTerraform; \
	fi
	terraform -chdir=terraform \
	  apply -auto-approve \
	  -var function_name="doc-sample-event-timer-csharp" \
	  -var function_handler_name=$(HANDLER_NAME) \
		-var runtime="C#(.NET Core 6.0)" \
		-var zip_file_name="sample_net6.0.zip" \
		-var prefix="csharp"


destroy:
	terraform -chdir=terraform \
	  destroy -auto-approve \
		-var function_name="doc-sample-event-timer-csharp" \
		-var function_handler_name=$(HANDLER_NAME) \
		-var runtime="C#(.NET Core 6.0)" \
		-var zip_file_name="sample_net6.0.zip" \
		-var prefix="csharp"

.PHONY: build zip clean all deploy destroy initTerraform

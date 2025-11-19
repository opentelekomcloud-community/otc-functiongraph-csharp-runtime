REM ########################################################################
REM Sample to upload picture to obs bucket using Huawei obsutil.
REM Huawei obsutil is available, see:
REM https://support.huaweicloud.com/intl/en-us/utiltg-obs/obs_11_0001.html
REM ########################################################################

REM for proxy use, set following environment variables
REM set HTTP_PROXY=proxy:port
REM set HTTPS_PROXY=proxy:port

REM For bucket name see output of terraform output
set OBS_INPUT_BUCKET="csharp-doc-sample-event-s3obs-thumbnail-csharp-images"

obsutil.exe cp .\test\resources\otc.jpg ^
  obs://%OBS_INPUT_BUCKET%/otc.jpg ^
  -e=https://obs.eu-de.otc.t-systems.com ^
  -i=%ACCESS_KEY% ^
  -k=%SECRET_ACCESS_KEY%

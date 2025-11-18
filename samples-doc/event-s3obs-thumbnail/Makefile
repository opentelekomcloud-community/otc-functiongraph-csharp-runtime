mkfile_path := $(abspath $(lastword $(MAKEFILE_LIST)))/..
current_dir := $(notdir $(patsubst %/,%,$(dir $(mkfile_path))))

FUNCTION_HANDLER_NAME := "s3obsThumbnail::src.Program::Handler"
FUNCTION_INITIALIZER_NAME := "s3obsThumbnail::src.Program::Initializer"
FUNCTION_NAME := "doc-sample-event-s3obs-thumbnail-csharp"
FUNCTION_RUNTIME := "C\#(.NET Core 6.0)"

FUNCTION_ZIP_FILE_NAME := event_s3obs_thumbnail_net6.0.zip
LOCAL_ZIP_FILE := $(mkfile_path)/src/$(FUNCTION_ZIP_FILE_NAME)
prefix := "csharp"


BACKEND_CONFIG_BUCKET := "sample-tf-backend"
BACKEND_CONFIG_KEY := "terraform_state/csharp/doc-sample-event-s3obs-thumbnail.tf"
BACKEND_CONFIG_REGION := "eu-de"
BACKEND_CONFIG_ENDPOINTS := "endpoints={s3=\"https://obs.eu-de.otc.t-systems.com\"}"

clean:
	rm -f $(mkfile_path)/src/*.zip
	rm -rf $(mkfile_path)/src/bin 
	rm -rf $(mkfile_path)/src/obj

build:
	dotnet build

all: build

release: 
	dotnet build -c Release

initTerraform:
	terraform -chdir=terraform \
	  init \
	  -backend-config=$(BACKEND_CONFIG_ENDPOINTS) \
	  -backend-config="bucket=$(BACKEND_CONFIG_BUCKET)" \
	  -backend-config="key=$(BACKEND_CONFIG_KEY)" \
	  -backend-config="region=$(BACKEND_CONFIG_REGION)"

plan: release
	if [ ! -f "terraform/.terraform.lock.hcl" ]; then \
		$(MAKE) initTerraform; \
	fi
	terraform -chdir=terraform \
	  plan \
	  -var function_name=$(FUNCTION_NAME) \
	  -var function_handler_name=$(FUNCTION_HANDLER_NAME) \
		-var function_initializer_name=$(FUNCTION_INITIALIZER_NAME) \
		-var function_runtime=$(FUNCTION_RUNTIME) \
		-var zip_file_name=$(FUNCTION_ZIP_FILE_NAME) \
		-var zip_file_local=$(LOCAL_ZIP_FILE) \
		-var prefix=$(prefix)

deploy: release
	if [ ! -f "terraform/.terraform.lock.hcl" ]; then \
		$(MAKE) initTerraform; \
	fi
	terraform -chdir=terraform \
	  apply -auto-approve \
	  -var function_name=$(FUNCTION_NAME) \
	  -var function_handler_name=$(FUNCTION_HANDLER_NAME) \
		-var function_initializer_name=$(FUNCTION_INITIALIZER_NAME) \
		-var function_runtime=$(FUNCTION_RUNTIME) \
		-var zip_file_name=$(FUNCTION_ZIP_FILE_NAME) \
		-var zip_file_local=$(LOCAL_ZIP_FILE) \
		-var prefix=$(prefix)


destroy:
	terraform -chdir=terraform \
	  destroy -auto-approve \
		-var function_name=$(FUNCTION_NAME) \
		-var function_handler_name=$(FUNCTION_HANDLER_NAME) \
		-var function_initializer_name=$(FUNCTION_INITIALIZER_NAME) \
		-var function_runtime=$(FUNCTION_RUNTIME) \
		-var zip_file_name=$(FUNCTION_ZIP_FILE_NAME) \
		-var zip_file_local=$(LOCAL_ZIP_FILE) \
		-var prefix=$(prefix)

.PHONY: build zip clean all release deploy destroy initTerraform plan

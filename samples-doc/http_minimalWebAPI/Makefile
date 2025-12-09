mkfile_path := $(abspath $(lastword $(MAKEFILE_LIST)))/..
current_dir := $(notdir $(patsubst %/,%,$(dir $(mkfile_path))))

BACKEND_CONFIG_BUCKET := "doc-samples-tf-backend"
BACKEND_CONFIG_KEY := "terraform_state/csharp/http_minimalWebAPI.tf"
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
	dotnet publish -c Release

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
		-var-file="http.tfvars"


deploy: release
	if [ ! -f "terraform/.terraform.lock.hcl" ]; then \
		$(MAKE) initTerraform; \
	fi
	terraform -chdir=terraform \
		apply -auto-approve \
		-var-file="http.tfvars"

deploy_nobuild: 
	@if [ ! -f "terraform/.terraform.lock.hcl" ]; then \
		 echo "backend not initialized..."; \
	else \
		terraform -chdir=terraform \
			apply -auto-approve \
			-var-file="http.tfvars"; \
	fi


destroy:
	terraform -chdir=terraform \
	  destroy -auto-approve \
		-var-file="http.tfvars"

.PHONY: build zip clean all release deploy destroy initTerraform plan

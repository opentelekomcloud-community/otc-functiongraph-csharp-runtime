# !/bin/bash
# Script to create a log stream index in OTC LTS for FunctionGraph Log Stream

OTC_TOKEN=$1
OTC_PROJECT_ID=$2
LOG_GROUP_ID=$3
LOG_STREAM_ID=$4


streamindex=$(cat << EOF
{
  "logStreamId": "$LOG_STREAM_ID",
  "sqlAnalysisEnable": true,
  "fullTextIndex": {
    "enable": true,
    "caseSensitive": true,
    "includeChinese": false,
    "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
    "ascii": []
  },
  "fields": [
    {
      "fieldName": "requestId",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "instanceId",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "stage",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "status",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "function",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "package",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "version",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "finishLog",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "memory",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "float",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "duration",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "float",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "cpu",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "float",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "storage",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "float",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "livedataTraceId",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    },
    {
      "fieldName": "errorType",
      "caseSensitive": false,
      "includeChinese": false,
      "tokenizer": ", '\";=()[]{}@&<>/:\\n\\t\\r",
      "quickAnalysis": true,
      "fieldType": "string",
      "fieldAnalysisAlias": null,
      "ltsSubFieldsInfoList": null,
      "ascii": []
    }
  ]
}
EOF
)

curl -X POST "https://lts.eu-de.otc.t-systems.com/v1.0/$OTC_PROJECT_ID/groups/$LOG_GROUP_ID/stream/$LOG_STREAM_ID/index/config" \
  -H "Content-Type: application/json;charset=utf8" \
  -H "X-Auth-Token: $OTC_TOKEN" \
  -d "$streamindex"

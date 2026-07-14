FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /source

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true

# Set timzone to UTC
ENV TZ=Etc/UTC

COPY --link ./src/*.csproj ./

# Restore with secrets and cache mounts (not stored in image)
RUN --mount=type=secret,id=github_nuget_username \
    --mount=type=secret,id=github_nuget_password \
    --mount=type=cache,target=/root/.nuget/packages \
    dotnet nuget add source \
      https://nuget.pkg.github.com/opentelekomcloud-community/index.json \
      --name="github" \
      --username $(cat /run/secrets/github_nuget_username) \
      --valid-authentication-types basic \
      --store-password-in-clear-text \
      --password $(cat /run/secrets/github_nuget_password) && \
    dotnet restore

COPY --link ./src ./
# Build and publish a release
RUN dotnet publish -c Release -o /app


# Build runtime image

#FROM mcr.microsoft.com/dotnet/aspnet:9.0
# -> Image Size: 172 MB

FROM mcr.microsoft.com/dotnet/runtime-deps:9.0
# -> Image Size: 128 MB

ENV GROUP_ID=1003
ENV GROUP_NAME=paas_user

ENV USER_ID=1003
ENV USER_NAME=paas_user

RUN groupadd -g $GROUP_ID $GROUP_NAME && \
    useradd -u $USER_ID -g $GROUP_NAME $USER_NAME

# Set timzone to UTC
ENV TZ=Etc/UTC

# functiongraph requires to listen on port 8000
ENV ASPNETCORE_HTTP_PORTS=8000

WORKDIR /app

#COPY --from=build /app .
# Only copy the binary, skip .pdb, .xml and other artifacts
COPY --from=build /app/container_event_timer .

RUN chown -R $USER_ID:$GROUP_ID /app && \
    chmod +x /app/container_event_timer

USER $USER_NAME

EXPOSE 8000

ENTRYPOINT ["/app/container_event_timer"]

FROM microsoft/dotnet:2.1-sdk
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN dotnet build -c Release
EXPOSE 80/tcp
RUN chmod +x ./entrypoint.sh_old
CMD /bin/bash ./entrypoint.sh_old
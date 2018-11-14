FROM microsoft/dotnet:2.1-sdk
RUN apt update
RUN apt install -y nginx

WORKDIR /app
COPY bin/Release/netcoreapp2.1/publish .

COPY ./entrypoint.sh .
RUN chmod +x ./entrypoint.sh

RUN rm /etc/nginx/nginx.conf
COPY nginx.conf /etc/nginx

ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000 80
CMD /bin/bash ./entrypoint.sh
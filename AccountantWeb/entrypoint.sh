#!/bin/bash

service nginx start
sleep 1

nginx -t

dotnet /app/Accountant.dll
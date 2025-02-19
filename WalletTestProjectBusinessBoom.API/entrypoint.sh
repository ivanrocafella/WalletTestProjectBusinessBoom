#!/bin/bash
set -e

echo "Applying database migrations..."
dotnet ef database update

echo "Starting application..."
exec dotnet WalletTestProjectBusinessBoom.API.dll
#!/bin/bash

set -e
run_cmd="dotnet OzonEdu.StockApi.dll --no-build -v d"

dotnet OzonEdu.StockApi.Migrator.dll --no-build -v d -- --dryrun

dotnet OzonEdu.StockApi.Migrator.dll --no-build -v d

>&2 echo "StockApi DB Migrations complete, starting app."
>&2 echo "Run StockApi: $run_cmd"
exec $run_cmd
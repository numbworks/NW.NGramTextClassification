FROM mcr.microsoft.com/devcontainers/dotnet@sha256:e4fbb49c3ee92933e40c6974b42dda3051827b7ff932463edbd0b575f63a612c

RUN apt -y update && \
    apt -y upgrade && \
    apt -y install git && \
    apt -y install go-md2man

RUN wget -nv https://github.com/numbworks/nwcommitaverages/releases/download/v2.0.1/nwcavg-v2.0.1-linux-amd64.deb -O /tmp/nwcavg-v2.0.1-linux-amd64.deb
RUN dpkg -i /tmp/nwcavg-v2.0.1-linux-amd64.deb

COPY data/TemplateClass /opt/templates/TemplateClass
RUN dotnet new install /opt/templates/TemplateClass

USER vscode
RUN dotnet tool install -g dotnet-reportgenerator-globaltool
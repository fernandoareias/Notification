FROM grafana/grafana:7.3.1 as ap-grafana
COPY  ./config/grafana.ini /etc/grafana
COPY  ./provisioning /etc/grafana/provisioning
COPY  ./dashboards /var/lib/grafana/dashboards
EXPOSE 3000
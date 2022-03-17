#!/bin/bash
cd /home/ec2-user/dataciense-project-scripts
sudo chmod +x stop.sh
sudo chmod +x clear.sh
sudo chmod +x start.sh
docker rmi eugennn/data-sciense-project:latest
docker pull eugennn/data-sciense-project:latest
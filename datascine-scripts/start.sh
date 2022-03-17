#!/bin/bash
cd /home/ec2-user/dataciense-project-scripts
sudo chmod +x stop.sh
sudo chmod +x clear.sh
sudo chmod +x start.sh

docker run -d --name data-sciense-project -p 4400:80 eugennn/data-sciense-project:latest
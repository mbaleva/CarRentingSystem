pipeline {
    agent any

    stages {
        stage('Print branch') {
            steps {
                echo "$GIT_BRANCH"
            }
        }
	stage('Docker Build') {
            steps {
                powershell(script: "ls")
                powershell(script: "cd ./Servers/CarRentingSystem")
                powershell(script: "ls")
                powershell(script: 'docker-compose build')
                powershell(script: "cd ..")
                powershell(script: "cd ..")
            }
        }
	stage('Push images') {
            steps {
		    script {
			    docker.withRegistry('https://index.docker.io/v1/', 'DockerHub'){
			    	def recipesweb = docker.image("mbaleva/recipesweb")
				    recipesweb.push("1.0.${env.BUILD_ID}")
				    recipesweb.push('latest')
			    }
		    }
            }
        }
    }
}
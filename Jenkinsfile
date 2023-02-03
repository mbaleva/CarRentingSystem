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
                powershell(script: 'docker-compose build')
            }
        }
    stage('Run application') {
        steps {
            powershell(script: 'docker-compose up -d')
        }
    }
    stage('Run PowerShell Tests') {
        steps {
            powershell(script: """
            cd tests
            cd powershell-tests
            .\init.ps1
            cd ..
            cd ..
            """)
        }
    }
    stage('Shut down application') {
        steps {
            powershell(script: 'docker-compose down')
        }
    }
	stage('Push images') {
            steps {
		    script {
			    docker.withRegistry('https://index.docker.io/v1/', 'DockerHub'){
                        def images = ["carrentingsystemclient", "carrentingsystemidentity", "carrentingsystemcars", "carrentingsystemanalyses", "carrentingsystemhealthchecks", "carrentingsystemrenting"]
			    	    images.each() {
                            def currentImage = docker.image("mbaleva/" + it)
                            currentImage.push("latest")
                            currentImage.push("1.0.${env.BUILD_ID}")
                        }
			        }
		        }
            }
        }
    }
}
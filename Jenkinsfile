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
	stage('Push images') {
            steps {
		    script {
			    docker.withRegistry('https://index.docker.io/v1/', 'DockerHub'){
                        def images = ["carrentingsystemclient", "carrentingsystemidentity", "carrentingsystemcars", "carrentingsystemanalyses", "carrentingsystemhealthchecks", "carrentingsystemrenting"]
			    	    images.each() {
                            def currentImage = docker.image("mbaleva/" + it)
                            currentImage.push("latest")
                        }
                        def carrentingsystemclient = docker.image("mbaleva/carrentingsystemclient")
				        carrentingsystemclient.push("1.0.${env.BUILD_ID}")
				        carrentingsystemclient.push('latest')
			        }
		        }
            }
        }
    }
}
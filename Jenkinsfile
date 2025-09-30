pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
            args '-v /var/jenkins_home:/workspace'
        }
    }
    
    environment {
        DOTNET_VERSION = '8.0'
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        
        stage('Setup .NET') {
            steps {
                sh 'dotnet --version'
            }
        }
        
        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }
        
        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release --no-restore'
            }
        }
        
        stage('Test') {
            steps {
                sh 'dotnet test --verbosity normal --no-build --configuration Release'
            }
        }
        
        stage('Publish') {
            steps {
                sh 'dotnet publish -c Release -o ./publish'
                
                // Archive the build output
                archiveArtifacts artifacts: 'publish/**/*', fingerprint: true
            }
        }
        
        stage('Docker Build') {
            agent any
            steps {
                script {
                    // Only build Docker if Dockerfile exists
                    if (fileExists('Dockerfile')) {
                        sh 'docker --version'
                        sh "docker build -t assurance-backend:${env.BUILD_ID} ."
                    } else {
                        echo 'Dockerfile not found. Skipping Docker build.'
                    }
                }
            }
        }
    }
    
    post {
        always {
            cleanWs()
        }
        success {
            echo '🎉 .NET Backend built successfully!'
            emailext (
                subject: "SUCCESS: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
                body: "The build was successful!\n\nCheck console output at: ${env.BUILD_URL}",
                to: "maissa.hammami@esprit.tn"
            )
        }
        failure {
            echo '❌ Build failed! Check console output for details.'
            emailext (
                subject: "FAILED: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
                body: "The build failed!\n\nCheck console output at: ${env.BUILD_URL}",
                to: "maissa.hammami@esprit.tn"
            )
        }
    }
}
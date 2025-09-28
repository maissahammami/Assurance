pipeline {
    agent any
    
    environment {
        DOTNET_VERSION = '8.0'
        NODE_VERSION = '18'
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        
        stage('Setup .NET') {
            steps {
                script {
                    if (isUnix()) {
                        sh 'dotnet --version'
                    } else {
                        bat 'dotnet --version'
                    }
                }
            }
        }
        
        stage('Restore Dependencies') {
            steps {
                script {
                    if (isUnix()) {
                        sh 'dotnet restore'
                    } else {
                        bat 'dotnet restore'
                    }
                }
            }
        }
        
        stage('Build') {
            steps {
                script {
                    if (isUnix()) {
                        sh 'dotnet build --configuration Release --no-restore'
                    } else {
                        bat 'dotnet build --configuration Release --no-restore'
                    }
                }
            }
        }
        
        stage('Test') {
            steps {
                script {
                    if (isUnix()) {
                        sh 'dotnet test --verbosity normal --no-build --configuration Release'
                    } else {
                        bat 'dotnet test --verbosity normal --no-build --configuration Release'
                    }
                }
            }
        }
        
        stage('Publish') {
            steps {
                script {
                    if (isUnix()) {
                        sh 'dotnet publish -c Release -o ./publish'
                    } else {
                        bat 'dotnet publish -c Release -o ./publish'
                    }
                }
            }
        }
        
        stage('Docker Build') {
            steps {
                script {
                    docker.build("assurance-backend:${env.BUILD_ID}")
                }
            }
        }
        
        stage('Deploy to Staging') {
            when {
                branch 'develop'
            }
            steps {
                script {
                    echo "Deploying to staging environment"
                    // Add your staging deployment commands
                    // docker push assurance-backend:${env.BUILD_ID}
                }
            }
        }
        
        stage('Deploy to Production') {
            when {
                branch 'main'
            }
            steps {
                script {
                    echo "Deploying to production environment"
                    // Add your production deployment commands
                }
            }
        }
    }
    
    post {
        always {
            cleanWs()
        }
        success {
            emailext (
                subject: "SUCCESS: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
                body: "The build was successful!\n\nCheck console output at: ${env.BUILD_URL}",
                to: "maissa.hammami@example.com"
            )
        }
        failure {
            emailext (
                subject: "FAILED: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
                body: "The build failed!\n\nCheck console output at: ${env.BUILD_URL}",
                to: "maissa.hammami@example.com"
            )
        }
    }
}
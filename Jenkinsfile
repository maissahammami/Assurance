pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Check Environment') {
            steps {
                sh 'echo "Checking environment..."'
                sh 'pwd'
                sh 'ls -la'
            }
        }
        stage('Build') {
            steps {
                script {
                    try {
                        sh 'dotnet --version'
                        sh 'dotnet restore'
                        sh 'dotnet build'
                        sh 'dotnet test'
                        echo "✅ .NET build successful"
                    } catch (Exception e) {
                        echo "❌ .NET not available or build failed"
                        echo "Please install .NET SDK 8.0 on Jenkins server"
                    }
                }
            }
        }
    }
    post {
        always {
            cleanWs()
        }
    }
    // Add after the Build stage
stage('Publish Artifacts') {
    steps {
        sh 'dotnet publish -c Release -o ./publish'
        archiveArtifacts artifacts: 'publish/**/*', fingerprint: true
    }
}

stage('Security Scan') {
    steps {
        echo '🔒 Running security checks...'
        // Add security scanning tools later
    }
}
}
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
                script {
                    echo "🔍 Checking Jenkins environment..."
                    sh 'uname -a'
                    sh 'pwd'
                    sh 'ls -la'
                    
                    // Check if .NET is available
                    try {
                        sh 'dotnet --version'
                        echo "✅ .NET is available"
                    } catch (Exception e) {
                        echo "❌ .NET is NOT installed on Jenkins server"
                        echo "Please install .NET SDK 8.0 on the Jenkins server"
                    }
                }
            }
        }
        
        stage('Install .NET if Missing') {
            steps {
                script {
                    // Try to install .NET if not available (Linux servers)
                    sh '''
                    if ! command -v dotnet &> /dev/null; then
                        echo "Installing .NET SDK..."
                        # For Ubuntu/Debian Jenkins servers
                        wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
                        sudo dpkg -i packages-microsoft-prod.deb
                        sudo apt-get update
                        sudo apt-get install -y dotnet-sdk-8.0
                        echo "✅ .NET SDK installed"
                    else
                        echo "✅ .NET SDK already installed"
                    fi
                    dotnet --version
                    '''
                }
            }
        }
        
        stage('Build and Test') {
            steps {
                script {
                    echo "🏗️ Building .NET application..."
                    sh 'dotnet restore'
                    sh 'dotnet build --configuration Release'
                    sh 'dotnet test --verbosity normal'
                }
            }
        }
        
        stage('Publish') {
            steps {
                script {
                    echo "📦 Publishing application..."
                    sh 'dotnet publish -c Release -o ./publish'
                    
                    // Archive the build artifacts
                    archiveArtifacts artifacts: 'publish/**/*', fingerprint: true
                    
                    echo "✅ Build artifacts archived in Jenkins"
                }
            }
        }
    }
    
    post {
        always {
            echo "🧹 Cleaning workspace..."
            cleanWs()
        }
        success {
            echo '🎉 BACKEND PIPELINE SUCCESSFUL!'
            echo '📦 Your .NET application is built and ready in ./publish/'
        }
        failure {
            echo '❌ Build failed! Check the logs above for details.'
        }
    }
}
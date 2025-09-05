pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
            args '-v $HOME/.nuget/packages:/root/.nuget/packages'
        }
    }

    stages {
        stage('Restore') {
            steps {
                sh 'echo "-----> Restaurando pacotes <-----"'
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'echo "-----> Compilando o projeto <-----"'
                sh 'dotnet build'
            }
        }

        stage('DB Update') {
            steps {
                sh 'echo "-----> Atualizando banco de dados <-----"'
                sh 'dotnet ef database update --project "$WORKSPACE/Services/RastreamentoPedidos.API" -- --environment Production'
            }
        }
    }
}

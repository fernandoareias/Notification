# Notification Sending Microservice

This repository contains a microservice project developed to manage and send notifications efficiently and scalably. Utilizing best software engineering practices, this service is designed with a focus on robustness, testability, and observability.

## Key Features:

1. **Microservices Architecture:** The project follows a microservices architecture, allowing each component to be independent and scalable, facilitating system maintenance and evolution.

2. **RabbitMQ for Request Queueing:** To ensure scalability and low latency, RabbitMQ is used to queue notification requests, ensuring a smooth processing flow even during peak loads.

3. **External API:** An external API has been developed to allow easy and flexible integration with other systems, facilitating the sending of notification requests to the microservice.

4. **MongoDB as Database:** MongoDB is used as the database to store relevant information about notifications, ensuring efficient persistence and retrieval of data.

5. **Rich Domain and DDD Practices:** The service is implemented using Rich Domain concepts and following Domain-Driven Design (DDD) practices, resulting in a more cohesive and business-oriented design.

6. **Comprehensive Testing:** The code is covered by a comprehensive suite of automated tests, ensuring the quality and reliability of the system. Unit, integration, and end-to-end tests are employed to validate the microservice's behavior in different scenarios.

7. **Observability:** The application features observability resources, including appropriate metrics and logging, ensuring that the performance and state of the system can be effectively monitored.

8. **Grafana for Monitoring:** We use Grafana to visualize and monitor important metrics related to RabbitMQ, the API, and Docker Compose containers, providing valuable insights into the system's performance and health.

## How to Run:

1. Clone this repository.
2. Make sure you have Docker and Docker Compose installed on your machine.
3. Navigate to the project's root directory and run `docker-compose up` to start the application.
4. Access the external API interface through the URL provided by the documentation.
5. Explore metrics and visualizations in Grafana to monitor the system in real-time.

## Contribution:

Contributions are welcome! Feel free to propose improvements, fix bugs, or add new features. Just open an issue or submit a pull request, and we'll be happy to review.

## License:

This project is licensed under the [MIT License](LICENSE).

## Contact:

For more information or support, contact [Fernando Areias](nando.calheirosx@gmail.com).

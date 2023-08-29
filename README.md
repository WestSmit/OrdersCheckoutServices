# OrdersCheckoutServices

To run the project make sure you have installed docker and docker-compose.

1. Execute ```docker-compose -f 'docker-compose.yml' -f 'docker-compose.override.yml' up``` at the root folder.
2. Use Swagger ```http://localhost:8080/swagger/index.html``` to send API requests.

Request body example for swagger:
```
{
  "userId": "85157167-2d19-43fd-ae41-0b4f97f7352e",
  "products": [
    {
      "productId": "9bb4be20-992d-4844-a2c8-c38e0ef10f70",
      "qty": 2
    }
  ]
}
```
RabbitMq
http://localhost:15672/#/,
username: `rmuser`
password: `rmpassword`

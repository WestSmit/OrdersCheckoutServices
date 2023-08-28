# OrdersCheckoutServices

To run the project make sure you have installed docker and docker-compose.

1. Execute ```docker-compose up``` at the root folder.
2. Use Swagger to send API request.

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

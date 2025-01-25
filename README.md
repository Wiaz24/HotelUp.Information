# HotelUp - Information service
![Application tests](https://github.com/Wiaz24/HotelUp.Information/actions/workflows/tests.yml/badge.svg)
![Github issues](https://img.shields.io/github/issues/Wiaz24/HotelUp.Information)
[![Docker Image Size](https://badgen.net/docker/size/wiaz/hotelup.information?icon=docker&label=image%20size)](https://hub.docker.com/r/wiaz/hotelup.information/)

This service should expose endpoints on port `5003` starting with:
```http
/api/information/
```

## Health checks
Health status of the service should be available at:
```http
/api/information/_health
```
and should return 200 OK if the service is running, otherwise 503 Service Unavailable.

## Message broker
This service uses `MassTransit` library to communicate with the message broker. For the purpose of integration with
another MassTransit service, all published events are declared in the `HotelUp.Information.Services.Events` namespace.

### AMQP Queues
This service creates queues that consume messages from the following exchanges:
- `HotelUp.Customer:RoomCreatedEvent` from [HotelUp.Customer](https://github.com/Wiaz24/HotelUp.Customer)
- `HotelUp.Customer:ReservationCreatedEvent` from [HotelUp.Customer](https://github.com/Wiaz24/HotelUp.Customer)
- `HotelUp.Customer:ReservationCanceledEvent` from [HotelUp.Customer](https://github.com/Wiaz24/HotelUp.Customer)
- `HotelUp.Kitchen:MenuCreatedEvent` from [HotelUp.Kitchen](https://github.com/Wiaz24/HotelUp.Kitchen)
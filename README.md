# eShop
Belajar Microservice

### Informasi Server Back End

API Gateway
```http://192.168.1.100:5100```

API Services (dibuka untuk kebutuhan testing saja)
* Catalog API ```http://192.168.1.100:5101```
* Cart API ```http://192.168.1.100:5102```

### Informasi Database
| Host        | Value           |
| ------------- |:-------------:|
| Host | 192.168.1.102\M2017 |
| Database Catalog | CatalogApi |
| Database Shopping Cart | ShoppingCartApi |

### Git Repository
Bisa di clone di URL berikut ```https://github.com/hskartono/eShop```

Masing-masing server menggunakan docker, untuk orchrestation-nya pakai docker compose
Untuk menjalankannya:

```shell
git clone https://github.com/hskartono/eShop
cd eShop
docker-compose build
docker-compose up
```

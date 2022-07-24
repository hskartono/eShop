# eShop
Source Code ini merupakan PoC dalam proses pembelajaran membuat arsitektur Micro Services.
Teknologi yang digunakan:
- ASP.NET Core Menggunakan .Net 6
- Ocelot sebagai API Gateway
- Docker
- Docker Compose
- SQL Server Database

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
---

Berikut adalah daftar Endpoint API Gateway yang dapat di hit oleh Front end untuk menjalankan proses pencarian barang, melihat detail barang, menambahkan ke dalam cart, menambah jumlah barang, mengurangi jumlah barang dan melakukan checkout.

| API Endpoint | Parameter | Keterangan |
|--------------|----------|-----------|
| GET /product?``` q ```=&``` categoryId ```= | Parameter ``` q ``` Berisi keyword nama barang yang akan di cari. Parameter ``` categoryId ``` Berisi id kategori barang yang akan di cari. | Digunakan untuk mendapatkan daftar produk dengan filter berdasarkan nama produk atau kombinasi dengan kategori produk. |
| GET /product/``` {productid} ```/``` {storeid} ``` | ``` {productid} ``` Berisi id produk yang akan dilihat detailnys. ``` {storeid} ``` Berisi id store lokasi dari produk yang akan dilihat detailnya. | Digunakan untuk mengambil data detail produk. |
| GET /cart | Tanpa parameter | Mengambil data shopping cart dari user yang aktif. Jika cart belum ada, maka akan otomatis dibuat |
| POST /cart/add/``` {productid} ```/``` {storeid} ``` |  ``` {productid} ``` dan ``` {storeid} ``` | ``` {productid} ``` Berisi id produk yang akan ditambahkan ke cart. ``` {storeid} ``` Berisi id store sumber dari toko tempat produk dipesan. |
| POST /cart/addqty/``` {productid} ``` | ``` {productid} ``` | Digunakana untuk menambahkan qty dari produk yang sudah dimasukkan ke dalam cart. Jika produk belum ada, maka akan di skip. |
| POST /cart/subqty/``` {productid} ``` | ``` {productid} ``` | Digunakana untuk mengurangi qty dari produk yang sudah dimasukkan ke dalam cart. Jika produk belum ada, maka akan di skip. |
| GET /cart/checkout | Tanpa parameter | Digunakan untuk melakukan checkout shopping cart. |

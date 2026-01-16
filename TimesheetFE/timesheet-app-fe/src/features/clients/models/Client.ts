export class Client {
  clientId: string;
  name: string;
  address: string;
  city: string;
  postalCode: string;
  country: string;

  constructor(clientId: string, name: string, address: string, city: string, postalCode: string, country: string) {
    this.clientId = clientId;
    this.name = name;
    this.address = address;
    this.city = city;
    this.postalCode = postalCode;
    this.country = country;
  }

}

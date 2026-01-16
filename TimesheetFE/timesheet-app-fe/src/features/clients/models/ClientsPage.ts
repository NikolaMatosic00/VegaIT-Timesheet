import { Client } from "./Client"

export class ClientsPage {
  pages: number
  clients: Client[]

  constructor(pages: number, clients: Client[]) {
    this.pages = pages
    this.clients = clients
  }
}

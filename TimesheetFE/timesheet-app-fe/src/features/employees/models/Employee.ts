export class Employee {

    id: string
    name: string
    username: string
    password: string
    email: string
    hoursPerWeek: string
    status: string
    role: string

    constructor(id: string, name: string, username: string, password: string, email: string, hoursPerWeek: string, status: string, role: string) {
        this.id = id
        this.name = name
        this.username = username
        this.password = password
        this.email = email
        this.hoursPerWeek = hoursPerWeek
        this.status = status
        this.role = role
    }

}
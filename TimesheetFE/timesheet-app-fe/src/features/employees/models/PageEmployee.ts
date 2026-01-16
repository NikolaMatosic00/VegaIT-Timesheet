import { Employee } from "./Employee";

export class PageEmployee {

    totalPagesCount: number;
    employeesList: Array<Employee>;

    constructor(totalPagesCount: number, employeesList: Array<Employee>) {
        this.totalPagesCount = totalPagesCount;
        this.employeesList = employeesList;
      }
     

}

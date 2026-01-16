export class Filter {

    firstLetter: string
    name: string
    pageSize: number
    pageNumber: number

    constructor(firstLetter: string, name: string, pageSize: number, pageNumber: number) {
        this.firstLetter = firstLetter;
        this.name = name;
        this.pageSize = pageSize;
        this.pageNumber = pageNumber;
    }


}

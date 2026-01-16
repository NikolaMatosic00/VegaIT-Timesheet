import { Category } from "./Category";

export class PageRequestCategory {

    totalPagesCount: number;
    categoriesList: Array<Category>;

    constructor(totalPagesCount: number, categoriesList: Array<Category>) {
        this.totalPagesCount = totalPagesCount;
        this.categoriesList = categoriesList;
      }
     

}

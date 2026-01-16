import { Category } from "./Category"

export class CategoriesPage {
    pages: number
    categories: Category[]

    constructor(pages: number, categories: Category[]){
        this.pages = pages
        this.categories = categories
    }
}
import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { Category } from './models/Category'
import { Filter } from '../../common/classes/Filter'
import { CategoriesPage } from './models/CategoriesPage'

export interface CategoriesPageState {
    categories: Category[]
    numberOfPages: number
    editableCategory: Category
    newCategory: Category
    filter: Filter
    deleteCategoryID: string
}

const initialState: CategoriesPageState = {
    categories: [],
    numberOfPages: 1,
    editableCategory: {} as Category,
    newCategory: {} as Category,
    filter: { firstLetter: "", name: "", pageSize: 3, pageNumber: 1 },
    deleteCategoryID: ""
}

export const CategorySlice = createSlice({
    name: 'categorySliceName',
    initialState,
    reducers: {
        getCategoryPages(state, action: PayloadAction<Filter>) {
            state.filter = action.payload
        },
        categoriesFetched(state, action: PayloadAction<CategoriesPage>) {
            state.categories = action.payload.categories
            state.numberOfPages = action.payload.pages
        },
        createCategory(state, action: PayloadAction<Category>) {
            state.newCategory = action.payload
        },
        categoryCreated(state, aciton: PayloadAction<Category>) {
            state.newCategory = {} as Category;
        },
        editCategory(state, action: PayloadAction<Category>) {
            state.editableCategory = action.payload
        },
        deleteCategory(state, action: PayloadAction<string>) {
            state.deleteCategoryID = action.payload
        }

    },
})

// Action creators are generated for each case reducer function
export const { getCategoryPages, categoriesFetched, createCategory, editCategory, deleteCategory } = CategorySlice.actions

export default CategorySlice.reducer;
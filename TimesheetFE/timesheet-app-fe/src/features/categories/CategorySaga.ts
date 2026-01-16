import { call, put, takeEvery, select } from 'redux-saga/effects'
import { Category } from './models/Category';
import { create, deleteCategoryByID, edit, getPage } from './CategoryService';
import { categoriesFetched, createCategory, getCategoryPages, editCategory, deleteCategory } from './CategoriesSlice';
import { CategoriesPage } from './models/CategoriesPage';
import { RootState } from '../../store';
import { Filter } from '../../common/classes/Filter';

const getFilters = (state: RootState) => state.categories.filter;
const getNewCategory = (state: RootState) => state.categories.newCategory;
const getEditableCategory = (state: RootState) => state.categories.editableCategory;
const getDeleteCategoryId = (state: RootState) => state.categories.deleteCategoryID;

function* workGetCategoriesFetch() {
    const filteri: Filter = yield select(getFilters)
    const categoriesPageResponse: { data: { totalPagesCount: number, items: [] } } = yield call(
        getPage, filteri.firstLetter, filteri.name, filteri.pageSize, filteri.pageNumber);
    const categoriesPage: CategoriesPage = new CategoriesPage(categoriesPageResponse.data.totalPagesCount, categoriesPageResponse.data.items)
    yield put(categoriesFetched(categoriesPage));
}

function* workCreateCategory() {
    const category: Category = yield select(getNewCategory)
    yield call(create, category);
}

function* workEditCategory() {
    const category: Category = yield select(getEditableCategory)
    yield call(edit, category);
}

function* workDeleteCategory() {
    const deleteCategoryId: string = yield select(getDeleteCategoryId)
    yield call(deleteCategoryByID, deleteCategoryId);
}


//takeEvery pregledaj takeLast etc.
export function* getCategoryPagesSaga() {
    yield takeEvery(getCategoryPages.type, workGetCategoriesFetch)
}

export function* createCategorySaga() {
    yield takeEvery(createCategory.type, workCreateCategory)
}

export function* editCategorySaga() {
    yield takeEvery(editCategory.type, workEditCategory)
}

export function* deleteCategorySaga() {
    yield takeEvery(deleteCategory.type, workDeleteCategory)
}

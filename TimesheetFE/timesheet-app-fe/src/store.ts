import { configureStore } from '@reduxjs/toolkit'
import createSagaMiddleware from 'redux-saga'
import clientReducer from './features/clients/ClientsSlice'
import { createClientSaga, deleteClientSaga, editClientSaga, getClientPagesSaga } from './features/clients/ClientSaga'
import { createCategorySaga, deleteCategorySaga, editCategorySaga, getCategoryPagesSaga } from './features/categories/CategorySaga'
import categoryReducer from './features/categories/CategoriesSlice'



const saga = createSagaMiddleware()
export const store = configureStore({
  reducer: {
    clients: clientReducer,
    categories: categoryReducer
  },
  middleware: [saga]
})

saga.run(getCategoryPagesSaga);
saga.run(createCategorySaga);
saga.run(editCategorySaga);
saga.run(deleteCategorySaga);

saga.run(getClientPagesSaga);
saga.run(createClientSaga);
saga.run(editClientSaga);
saga.run(deleteClientSaga);

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch

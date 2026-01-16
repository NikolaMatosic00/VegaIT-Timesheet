import { useRef, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Filter } from './common/classes/Filter'
import { createCategory, getCategoryPages, editCategory, deleteCategory } from './features/categories/CategoriesSlice'
import { Category } from './features/categories/models/Category'
import { getAllClients } from './features/clients/ClientService'
import { RootState } from './store'

function NovaKomp() {

    // const currentCl = useSelector((state:RootState) => state.clients.editableClient)
    // const sviCl = useSelector((state: RootState) => state.clients.clients)
    // const [sviCli, setSviCl] = useState([]);
    const firstLetter = useRef() as React.MutableRefObject<HTMLInputElement>
    const name = useRef() as React.MutableRefObject<HTMLInputElement>
    const pageSize = useRef() as React.MutableRefObject<HTMLInputElement>
    const pageNumber = useRef() as React.MutableRefObject<HTMLInputElement>
    const id = useRef() as React.MutableRefObject<HTMLInputElement>
    const categoryName = useRef() as React.MutableRefObject<HTMLInputElement>
    const deleteCategoryId = useRef() as React.MutableRefObject<HTMLInputElement>

    const dispatch = useDispatch();

    const dobavljeneStrane = useSelector((state: RootState) => state.categories)
    const numOfPages = useSelector((state: RootState) => state.categories.numberOfPages)
    const newCategory = useSelector((state: RootState) => state.categories.newCategory)
    const editableCategory = useSelector((state: RootState) => state.categories.editableCategory)

    function fja() {
        let filteri = new Filter(firstLetter.current.value, name.current.value, parseInt(pageSize.current.value), parseInt(pageNumber.current.value))
        console.log(filteri)
        if (!isNaN(parseInt(pageSize.current.value))) {
            dispatch(getCategoryPages(filteri))
        } else {
            console.log("Nije unet broj")
        }
    }

    function fja2() {
        dispatch(createCategory(new Category(id.current.value, categoryName.current.value)))
    }

    function fja3() {
        dispatch(editCategory(new Category(id.current.value, categoryName.current.value)))
    }

    function fja4() {
        dispatch(deleteCategory(deleteCategoryId.current.value))
    }

    return (
        <div>
            <h2>Categories TEST</h2>
            <input type="text" ref={firstLetter} placeholder="first letter" />
            <input type="text" ref={name} placeholder="name" />
            <input type="number" ref={pageSize} placeholder="page size" />
            <input type="number" ref={pageNumber} placeholder="page number" />
            <button onClick={fja}>Dobavi sa beka</button>
            <button onClick={() => console.log(dobavljeneStrane)}>Klikni za prikaz svih u konzoli</button>
            <p>Number of pages:{numOfPages}</p>

            <br />
            CREATE<input type="text" ref={id} placeholder="category id" />
            <input type="text" ref={categoryName} placeholder="category name" />
            <button onClick={fja2}>Posalji na bek</button>
            <button onClick={() => console.log(newCategory)}>Klikni za prikaz newCategory u konzoli</button>


            <br />
            EDIT<input type="text" ref={id} placeholder="edit category id" />
            <input type="text" ref={categoryName} placeholder="edit category name" />
            <button onClick={fja3}>Posalji na bek</button>
            <button onClick={() => console.log(editableCategory)}>Klikni za prikaz editableCategory u konzoli</button>


            <br />
            DELETE<input type="text" ref={deleteCategoryId} placeholder="delete category id" />
            <button onClick={fja4}>Obrisi kategoriju</button>
        </div>
    )
}

export default NovaKomp
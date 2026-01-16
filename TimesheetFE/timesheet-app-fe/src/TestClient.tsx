import { useRef, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Filter } from './common/classes/Filter'
import { createClient, getClientsPage, editClient, deleteClient } from './features/clients/ClientsSlice'
import { Client } from './features/clients/models/Client'
import { getAllClients } from './features/clients/ClientService'
import { RootState } from './store'

function TestClient() {
  const firstLetter = useRef() as React.MutableRefObject<HTMLInputElement>
    const name = useRef() as React.MutableRefObject<HTMLInputElement>
    const pageSize = useRef() as React.MutableRefObject<HTMLInputElement>
    const pageNumber = useRef() as React.MutableRefObject<HTMLInputElement>
    const id = useRef() as React.MutableRefObject<HTMLInputElement>
    const ClientName = useRef() as React.MutableRefObject<HTMLInputElement>
    const deleteClientId = useRef() as React.MutableRefObject<HTMLInputElement>

    const address = useRef() as React.MutableRefObject<HTMLInputElement>
    const addresss = useRef() as React.MutableRefObject<HTMLInputElement>
    const city = useRef() as React.MutableRefObject<HTMLInputElement>
    const postalCode = useRef() as React.MutableRefObject<HTMLInputElement>
    const country = useRef() as React.MutableRefObject<HTMLInputElement>


    const dispatch = useDispatch();

    const dobavljeneStrane = useSelector((state: RootState) => state.clients)
    const numOfPages = useSelector((state: RootState) => state.clients.numberOfPages)
    const newClient = useSelector((state: RootState) => state.clients.newClient)
    const editableClient = useSelector((state: RootState) => state.clients.editableClient)

    function fja() {
        let filteri = new Filter(firstLetter.current.value, name.current.value, parseInt(pageSize.current.value), parseInt(pageNumber.current.value))
        console.log(filteri)
        if (!isNaN(parseInt(pageSize.current.value))) {
            dispatch(getClientsPage(filteri))
        } else {
            console.log("Nije unet broj")
        }
    }

    function fja2() {
        const clienttt = new Client(id.current.value, addresss.current.value, address.current.value, city.current.value, postalCode.current.value, country.current.value)
        console.log(clienttt)
        dispatch(createClient(clienttt))
    }

    // function fja3() {
    //     dispatch(editClient(new Client(id.current.value, ClientName.current.value)))
    // }

    function fja4() {
        dispatch(deleteClient(deleteClientId.current.value))
    }

    return (
        <div>
            <h2>Clients TEST</h2>
            <input type="text" ref={firstLetter} placeholder="first letter" />
            <input type="text" ref={name} placeholder="name" />
            <input type="number" ref={pageSize} placeholder="page size" />
            <input type="number" ref={pageNumber} placeholder="page number" />
            <button onClick={fja}>Dobavi sa beka</button>
            <button onClick={() => console.log(dobavljeneStrane)}>Klikni za prikaz svih u konzoli</button>
            <p>Number of pages:{numOfPages}</p>

            <br />
            CREATE<input type="text" ref={id} placeholder="Client id" />
            <input type="text" ref={addresss} placeholder="Client name" />
            <input type="text" ref={address} placeholder="Client address" />
            <input type="text" ref={city} placeholder="Client city" />
            <input type="text" ref={postalCode} placeholder="Client postalCode" />
            <input type="text" ref={country} placeholder="Client country" />
            <button onClick={fja2}>Posalji na bek</button>
            <button onClick={() => console.log(newClient)}>Klikni za prikaz newClient u konzoli</button>

{/* 
            <br />
            EDIT<input type="text" ref={id} placeholder="edit Client id" />
            <input type="text" ref={ClientName} placeholder="edit Client name" />
            <button onClick={fja3}>Posalji na bek</button>
            <button onClick={() => console.log(editableClient)}>Klikni za prikaz editableClient u konzoli</button>
 */}

            <br />
            DELETE<input type="text" ref={deleteClientId} placeholder="delete Client id" />
            <button onClick={fja4}>Obrisi kategoriju</button>
        </div>
    )
}

export default TestClient
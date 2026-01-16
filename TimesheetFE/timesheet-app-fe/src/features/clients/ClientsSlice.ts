import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { Client } from './models/Client'
import { Filter } from '../../common/classes/Filter'
import { ClientsPage } from './models/ClientsPage'

export interface ClientsPageState {
  clients: Client[]
  numberOfPages: number
  editableClient: Client
  newClient: Client
  filter: Filter
  deleteClientID: string
}

const initialState: ClientsPageState = {
  clients: [],
  numberOfPages: 1,
  editableClient: {} as Client,
  newClient: {} as Client,
  filter: { firstLetter: "", name: "", pageSize: 3, pageNumber: 1 },
  deleteClientID: ""
}

export const clientSlice = createSlice({
  name: 'clientSliceName',
  initialState,
  reducers: {
    getClientsPage(state, action: PayloadAction<Filter>) {
      state.filter = action.payload
    },
    clientsFetched(state, action: PayloadAction<ClientsPage>) {
      state.clients = action.payload.clients
      state.numberOfPages = action.payload.pages
    },
    createClient(state, action: PayloadAction<Client>) {
      state.newClient = action.payload
    },
    editClient(state, action: PayloadAction<Client>) {
      state.editableClient = action.payload
    },
    deleteClient(state, action: PayloadAction<string>) {
      state.deleteClientID = action.payload
    }

  },
})

// Action creators are generated for each case reducer function
export const { getClientsPage, clientsFetched, createClient, editClient, deleteClient } = clientSlice.actions

export default clientSlice.reducer;
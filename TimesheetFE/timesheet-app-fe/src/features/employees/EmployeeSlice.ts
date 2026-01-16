import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { Employee } from './models/Employee'
import { Filter } from '../../common/classes/Filter'


export interface EmployeesPageState {
  clients: Employee[]
  editableEmployee: Employee
  filter: Filter
}

const initialState: EmployeesPageState = {
  clients: new Array<Employee>,
  editableEmployee: { id: "", name: "", username: "", password: "", email: "", hoursPerWeek: "", status: "", role: ""},
  filter: { firstLetter: "", name: "", pageSize: 3, pageNumber: 1 }
}

export const employeeSlice = createSlice({
  name: 'employeeSliceName',
  initialState,
  reducers: {
  },
})

// Action creators are generated for each case reducer function
export const { } = employeeSlice.actions

export default employeeSlice.reducer;
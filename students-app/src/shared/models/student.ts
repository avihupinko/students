
export type SortColumn = keyof Student | '';
export type SortDirection = 'asc' | 'desc' | '';

export interface SortEvent {
	column: SortColumn;
	direction: SortDirection;
}

export interface Student {
    id: number
    studentId: string
    oldStudentId: string
    firstName: string
    lastName: string
    gender: string
    studentClass: string
    studentStatus: string
    studentType: string
    surveyStatus?: string
    personalPlanStatus: any
    birthDate: string
    birthCountry: string
  }
  
  export interface Country {
    id: number
    name: string
  }

export interface SearchResult<T> {
    total: number;
    data: T[]
}
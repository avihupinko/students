import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject, debounceTime, delay, switchMap, tap } from "rxjs";
import { LoaderService } from "src/shared/loader.service";
import { SearchResult, Student } from "src/shared/models/student";
import { StudentsService } from "src/shared/users.service";

interface State {
    page: number;
    pageSize: number;
    firstName: string;
    sort: string;
}


@Injectable({
    providedIn: 'root',
})
export class TableService {
    private _state: State = {
        page: 1,
        pageSize: 10,
        firstName: '',
        sort: ''
    };


    private _search$ = new Subject<void>();
    private _data$ = new BehaviorSubject<Student[]>([]);
    private _total$ = new BehaviorSubject<number>(0);

    constructor(private service: StudentsService,
        private loader: LoaderService) {
        this._search$
            .pipe(
                tap(() => this.loader.setLoading(true)),
                debounceTime(200),
                switchMap(() => this._search()),
                delay(200),
            )
            .subscribe((result) => {
                this._data$.next(result.data);
                this._total$.next(result.total);
                this.loader.setLoading(false)
            });

        this._search$.next();
    }

    get data$() {
        return this._data$.asObservable();
    }
    get total$() {
        return this._total$.asObservable();
    }

    get page() {
        return this._state.page;
    }
    get pageSize() {
        return this._state.pageSize;
    }
    get firstName() {
        return this._state.firstName;
    }

    set page(page: number) {
        this._set({ page });
    }
    set pageSize(pageSize: number) {
        this._set({ pageSize });
    }
    set firstName(firstName: string) {
        this._set({ firstName });
    }

    set sort(sort: string) {
        this._set({ sort });
    }

    private _set(patch: Partial<State>) {
        Object.assign(this._state, patch);
        this._search$.next();
    }

    public refresh(){
        this._search$.next();
    }

    private _search(): Observable<SearchResult<Student>> {
        const { pageSize, page, firstName, sort } = this._state;
        return this.service.get(firstName, pageSize, page - 1, sort);
    }
}
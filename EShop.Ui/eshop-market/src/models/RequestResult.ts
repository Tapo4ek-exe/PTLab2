export class RequestResult<T> {
    IsSuccess = true;
    Errors: Array<string> = [];
    Data?: T;

    public toString = (): string => JSON.stringify(this);
}

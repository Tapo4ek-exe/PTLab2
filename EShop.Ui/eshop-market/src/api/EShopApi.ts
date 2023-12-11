import axios, { AxiosInstance, AxiosResponse } from "axios";
import { Product } from "../models/Product";
import { RequestResult } from "../models/RequestResult";
import { ApiBaseUrl, JwtTokenKey } from "../Constants";

export class EShopApi {
  private _httpClient: AxiosInstance;
  private _jwtToken: string;

  constructor() {
    this._httpClient = axios.create({
      baseURL: ApiBaseUrl.endsWith("/") ? ApiBaseUrl : `${ApiBaseUrl}/`,
      headers: { "Content-Type": "application/json" },
    });
    this._jwtToken = localStorage.getItem(JwtTokenKey) ?? "";
  }

  checkUserIsLoggedIn(): boolean {
    return this._jwtToken !== "";
  }

  async login(email: string, password: string): Promise<RequestResult<string>> {
    const response = await this.post<string>("Auth/Login", { email: email, password: password });
    if (response.IsSuccess) {
        const token = response.Data ?? "";
        localStorage.setItem(JwtTokenKey, token);
        this._jwtToken = token;
    }
    return response;
  }

  async register(name: string, email: string, password: string): Promise<RequestResult<string>> {
    const response = await this.post<string>("Auth/Register", { name: name, email: email, password: password });
    if (response.IsSuccess) {
        const token = response.Data ?? "";
        localStorage.setItem(JwtTokenKey, token);
        this._jwtToken = token;
    }
    return response;
  }

  async logout() {
    localStorage.removeItem(JwtTokenKey);
  }

  async getUserName() {
    const response = await this.get<string>("Auth/GetUserName");
    return response;
  }

  async getProducts(): Promise<RequestResult<Product[]>> {
    const response = await this.get<Product[]>("Product/GetProducts");
    return response;
  }

  /**
   * Отправка GET запроса
   * @param method Метод для запроса
   * @param params Параметры для строки запроса
   * @returns Результат запроса
   */
  private async get<T>(
    method: string,
    params?: any
  ): Promise<RequestResult<T>> {
    return this.trySendRequest(() =>
      this._httpClient.get<T>(method, {
        params: params,
        headers: { Authorization: `Bearer ${this._jwtToken}` }
      })
    );
  }

  /**
   * Отправка POST запроса
   * @param method Метод для запроса
   * @param data Параметры для тела запроса
   * @param params Параметры для строки запроса
   * @returns Результат запроса
   */
  private async post<T>(
    method: string,
    data?: any,
    params?: any
  ): Promise<RequestResult<T>> {
    return this.trySendRequest(() =>
      this._httpClient.post<T>(method, data, {
        params: params,
        headers: { Authorization: `Bearer ${this._jwtToken}` }
      })
    );
  }

  private async trySendRequest<T>(
    request: () => Promise<AxiosResponse<T>>
  ): Promise<RequestResult<T>> {
    const result = new RequestResult<T>();
    try {
      const response = await request();

      if (this.checkResponseSuccess(response.status)) {
        result.Data = response.data;
      } else {
        result.IsSuccess = false;
        result.Errors.push(response?.statusText);
      }
    } catch (error) {
      result.IsSuccess = false;
      result.Errors.push((error as Error).message);
    }
    return result;
  }

  private checkResponseSuccess(status: number): boolean {
    return status >= 200 && status < 300;
  }
}

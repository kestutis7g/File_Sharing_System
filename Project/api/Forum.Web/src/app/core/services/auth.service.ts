import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequest } from '../types/auth.types';
import { decodeBase64, encodeBase64 } from '../utils/utils';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	constructor(private router: Router) {
		// Nothing
	}

	public isAuthenticated: boolean = false;

	private readonly TokenStorage: string = 'auth-token';
	private readonly LoginPage: string = '/login';
	private readonly DefaultPage: string = '/payment/list';
	private readonly TokenValidInMinutes: number = 60;

	public signIn(request: LoginRequest): boolean {
		if (request.username === 'Admin' && request.password === 'TheAdmin2') {
			this.isAuthenticated = true;

			const date = new Date();

			localStorage.setItem(this.TokenStorage, encodeBase64(date.toISOString()));

			this.router.navigate([this.DefaultPage]);

			return true;
		}

		this.isAuthenticated = false;

		return false;
	}

	public signOut(): void {
		this.isAuthenticated = false;
		localStorage.removeItem(this.TokenStorage);
		this.router.navigate([this.LoginPage]);
	}

	public validateSession(): void {
		const token = this.getToken();
		if (!token) {
			return;
		}
		const currentDate = new Date();

		const tokenDate = new Date(decodeBase64(token));
		tokenDate.setMinutes(tokenDate.getMinutes() + this.TokenValidInMinutes);

		if (currentDate.getTime() < tokenDate.getTime()) {
			this.isAuthenticated = true;
			this.router.navigate([this.DefaultPage]);
			return;
		}

		this.isAuthenticated = false;
		this.router.navigate([this.LoginPage]);
	}

	private getToken(): string | null {
		return localStorage.getItem(this.TokenStorage);
	}
}

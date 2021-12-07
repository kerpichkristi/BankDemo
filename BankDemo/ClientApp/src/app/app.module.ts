import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { UserService } from './users/user.service';
import { RegisterComponent } from './users/register.component';
import { LoginComponent } from './users/login.component';
import { AuthorizeInterceptor } from './authorize/authorize.interceptor';
import { AuthorizeGuard } from './authorize/authorize.guard';
import { AdministrationComponent } from './administration/administration.component';

import { UsersIndexComponent } from './users/index.component';
import { UsersListComponent } from './users/list.component';
import { UserDetailsComponent } from './users/details.component';
import { UserEditComponent } from './users/edit.component';

import { TransactionService } from './transactions/transactions.service';
import { TransactionsIndexComponent } from './transactions/index.component';
import { TransactionsListComponent } from './transactions/list.component';
//import { TransactionsDetailsComponent } from './transactions/details.component';
//import { TransactionsEditComponent } from './transactions/edit.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatDatepickerModule } from '@angular/material';
import { MatNativeDateModule } from '@angular/material';

import 'hammerjs';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,

    RegisterComponent,
    LoginComponent,
    AdministrationComponent,

    UsersIndexComponent,
    UserDetailsComponent,
    UserEditComponent,
    UsersListComponent,
    
    TransactionsIndexComponent,
    TransactionsListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },

      { path: 'users', component: UsersIndexComponent, canActivate: [AuthorizeGuard], data: { allowedRoles: ['Administrator'] } },
      { path: 'users/:id', component: UserDetailsComponent, canActivate: [AuthorizeGuard], data: { allowedRoles: ['Administrator'] } },
      { path: 'users/edit/:id', component: UserEditComponent, canActivate: [AuthorizeGuard], data: { allowedRoles: ['Administrator'] } },

      { path: 'users/register', component: RegisterComponent },
      { path: 'users/login', component: LoginComponent },
      { path: 'administration', component: AdministrationComponent, canActivate: [AuthorizeGuard], data: { allowedRoles: ['Administrator'] } },

      { path: 'transactions', component: TransactionsIndexComponent, canActivate: [AuthorizeGuard], data: { allowedRoles: ['Administrator'] }}

    ]),
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatTooltipModule,
    MatCardModule,
    MatButtonModule,
    MatDividerModule,
    MatListModule,
    MatNativeDateModule,
    MatDatepickerModule
  ],
  providers: [UserService, TransactionService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizeInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }

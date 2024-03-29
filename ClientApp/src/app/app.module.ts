import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { AssetsListComponent } from './userAssets/assets-list/assets-list.component';
import { AssetsDetailComponent } from './userAssets/assets-detail/assets-detail.component';
import { AssetsCardComponent } from './userAssets/assets-card/assets-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { AssetsEditComponent } from './userAssets/assets-edit/assets-edit.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoEditorComponent } from './userAssets/photo-editor/photo-editor.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { AssetsAddComponent } from './userAssets/assets-add/assets-add.component';
import { YesNoSelectComponent } from './_forms/yes-no-select/yes-no-select.component';
import {MatLegacySelectModule as MatSelectModule} from '@angular/material/legacy-select';
import { CommonModule } from '@angular/common';
import { PhotoEditorMemberComponent } from './members/photo-editor-member/photo-editor-member.component';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { UserConfigComponent } from './admin/user-config/user-config.component';
import { PhotoConfigComponent } from './admin/photo-config/photo-config.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { routeStrategy } from './_services/routeStrategy';
import { RouteReuseStrategy } from '@angular/router';
import { ConfirmPopupComponent } from './modals/confirm-popup/confirm-popup.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    ListsComponent,
    MessagesComponent,
    MemberDetailComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    AssetsListComponent,
    AssetsDetailComponent,
    AssetsCardComponent,
    AssetsEditComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TextInputComponent,
    AssetsAddComponent,
    YesNoSelectComponent,
    PhotoEditorMemberComponent,
    MemberMessagesComponent,
    AdminPanelComponent,
    HasRoleDirective,
    UserConfigComponent,
    PhotoConfigComponent,
    RolesModalComponent,
    ConfirmPopupComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    MatSelectModule,
    CommonModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: RouteReuseStrategy, useClass: routeStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

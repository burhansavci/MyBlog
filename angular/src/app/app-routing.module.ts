import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { AboutComponent } from './pages/about/about.component';
import { ContactComponent } from './pages/contact/contact.component';
import { ArticleResolver } from './resolvers/article.resolver';
import { ArticleComponent } from './pages/article/article.component';
import { ArticleArchiveResolver } from './resolvers/article-archive.resolver';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './admin/login/login.component';
import { ArticleListComponent } from './admin/article/article-list/article-list.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { ArticleFormComponent } from './admin/article/article-form/article-form.component';
import { CategoryListComponent } from './admin/category/category-list/category-list.component';
import { CategoryResolver } from './resolvers/category.resolver';
import { CategoryFormComponent } from './admin/category/category-form/category-form.component';
import { LanguageResolver } from './resolvers/language.resolver';
import { LanguageListComponent } from './admin/language/language-list/language-list.component';

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        component: HomeComponent,
        resolve: { dataResult: ArticleResolver },
      },
      {
        path: 'page/:page',
        component: HomeComponent,
        resolve: { dataResult: ArticleResolver },
      },
      {
        path: 'category/:categoryName/:categoryId',
        component: HomeComponent,
        resolve: { dataResult: ArticleResolver },
      },
      {
        path: 'category/:categoryName/:categoryId/page/:page',
        component: HomeComponent,
        resolve: { dataResult: ArticleResolver },
      },
      {
        path: 'archive/:year',
        component: HomeComponent,
        resolve: { dataResult: ArticleArchiveResolver },
      },
      {
        path: 'archive/:year/page/:page',
        component: HomeComponent,
        resolve: { dataResult: ArticleArchiveResolver },
      },
      {
        path: 'archive/:year/:month',
        component: HomeComponent,
        resolve: { dataResult: ArticleArchiveResolver },
      },
      {
        path: 'archive/:year/:month/page/:page',
        component: HomeComponent,
        resolve: { dataResult: ArticleArchiveResolver },
      },
      {
        path: 'article/:title/:id',
        component: ArticleComponent,
        resolve: { dataResult: ArticleResolver },
      },
      {
        path: 'about',
        component: AboutComponent,
      },
      {
        path: 'contact',
        component: ContactComponent,
      },
    ],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: AdminHomeComponent,
      },
      {
        path: 'article',
        children: [
          {
            path: 'list',
            component: ArticleListComponent,
            resolve: { dataResult: ArticleResolver },
          },
          {
            path: 'add',
            component: ArticleFormComponent,
          },
          {
            path: 'update/:id',
            component: ArticleFormComponent,
            resolve: { dataResult: ArticleResolver },
          },
        ],
      },
      {
        path: 'category',
        children: [
          {
            path: 'list',
            component: CategoryListComponent,
            resolve: { dataResult: CategoryResolver },
          },
          {
            path: 'add',
            component: CategoryFormComponent,
          },
          {
            path: 'update/:id',
            component: CategoryFormComponent,
            resolve: { dataResult: CategoryResolver },
          },
        ],
      },
      {
        path: 'language',
        children: [
          {
            path: 'list',
            component: LanguageListComponent,
            resolve: { dataResult: LanguageResolver },
          },
        ],
      },
    ],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}

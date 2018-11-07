var ROUTES_INDEX = {"name":"<root>","kind":"module","className":"AppModule","children":[{"name":"routes","filename":"src/app/app.routing.ts","module":"AppRoutingModule","children":[{"path":"","redirectTo":"dashboard","pathMatch":"full"},{"path":"404","component":"P404Component","data":{"title":"Page404"}},{"path":"500","component":"P500Component","data":{"title":"Page500"}},{"path":"login","component":"LoginComponent","data":{"title":"LoginPage"}},{"path":"register","component":"RegisterComponent","data":{"title":"RegisterPage"}},{"path":"","component":"DefaultLayoutComponent","data":{"title":"Home"},"children":[{"path":"base","loadChildren":"./views/base/base.module#BaseModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/base/base-routing.module.ts","module":"BaseRoutingModule","children":[{"path":"","data":{"title":"Base"},"children":[{"path":"cards","component":"CardsComponent","data":{"title":"Cards"}},{"path":"forms","component":"FormsComponent","data":{"title":"Forms"}},{"path":"switches","component":"SwitchesComponent","data":{"title":"Switches"}},{"path":"tables","component":"TablesComponent","data":{"title":"Tables"}},{"path":"tabs","component":"TabsComponent","data":{"title":"Tabs"}},{"path":"carousels","component":"CarouselsComponent","data":{"title":"Carousels"}},{"path":"collapses","component":"CollapsesComponent","data":{"title":"Collapses"}},{"path":"paginations","component":"PaginationsComponent","data":{"title":"Pagination"}},{"path":"popovers","component":"PopoversComponent","data":{"title":"Popover"}},{"path":"progress","component":"ProgressComponent","data":{"title":"Progress"}},{"path":"tooltips","component":"TooltipsComponent","data":{"title":"Tooltips"}}]}],"kind":"module"}],"module":"BaseModule"}]},{"path":"projects","loadChildren":"./views/projects/projects.module#ProjectsModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/projects/projects-routing.module.ts","module":"ProjectsRoutingModule","children":[{"path":"","data":{"title":"Projekter"},"children":[{"path":"view","component":"ViewProjectsComponent","data":{"title":"Visprojekter"}},{"path":"new","component":"NewProjectComponent","data":{"title":"Tilføjprojekt"}},{"path":"details/:id","component":"DetailsProjectComponent","data":{"title":"Detaljeromprojekt"}}]}],"kind":"module"}],"module":"ProjectsModule"}]},{"path":"customers","loadChildren":"./views/customers/customers.module#CustomersModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/customers/customers-routing.module.ts","module":"CustomersRoutingModule","children":[{"path":"","data":{"title":"Kunder"},"children":[{"path":"view","component":"ViewCustomersComponent","data":{"title":"Viskunder"}},{"path":"new","component":"NewCustomerComponent","data":{"title":"Tilføjkunde"}},{"path":"details/:id","component":"DetailsCustomerComponent","data":{"title":"Detaljerforkunde"}}]}],"kind":"module"}],"module":"CustomersModule"}]},{"path":"items","loadChildren":"./views/items/items.module#ItemsModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/items/items-routing.module.ts","module":"ItemsRoutingModule","children":[{"path":"","data":{"title":"Genstande"},"children":[{"path":"view","component":"ViewItemsComponent","data":{"title":"Visgenstande"}},{"path":"new","component":"AddItemsComponent","data":{"title":"Tilføjnygenstand"}},{"path":"details/:id","component":"DetailsItemComponent","data":{"title":"Detaljeromgenstand"}}]}],"kind":"module"}],"module":"ItemsModule"}]},{"path":"itemTemplates","loadChildren":"./views/itemTemplates/itemTemplates.module#ItemTemplatesModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/itemTemplates/itemTemplates-routing.module.ts","module":"ItemTemplatesRoutingModule","children":[{"path":"","data":{"title":"Skabeloner"},"children":[{"path":"view","component":"ViewItemTemplatesComponent","data":{"title":"Visskabeloner"}},{"path":"new","component":"NewItemTemplateComponent","data":{"title":"Tilføjskabelon"}},{"path":"details/:id","component":"DetailsItemTemplateComponent","data":{"title":"Visdetaljer"}}]}],"kind":"module"}],"module":"ItemTemplatesModule"}]},{"path":"users","loadChildren":"./views/users/users.module#UsersModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/users/users-routing.module.ts","module":"UsersRoutingModule","children":[{"path":"","data":{"title":"Brugere"},"children":[{"path":"view","component":"ViewUsersComponent","data":{"title":"Visbrugere"}},{"path":"new","component":"NewUserComponent","data":{"title":"Tilføjnybruger"}},{"path":"edit/:id","component":"EditUserComponent","data":{"title":"Redigerbruger"}},{"path":"details/:id","component":"DetailsUserComponent","data":{"title":"Detaljerombruger"}}]}],"kind":"module"}],"module":"UsersModule"}]},{"path":"buttons","loadChildren":"./views/buttons/buttons.module#ButtonsModule"},{"path":"charts","loadChildren":"./views/chartjs/chartjs.module#ChartJSModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/chartjs/chartjs-routing.module.ts","module":"ChartJSRoutingModule","children":[{"path":"","component":"ChartJSComponent","data":{"title":"Charts"}}],"kind":"module"}],"module":"ChartJSModule"}]},{"path":"dashboard","loadChildren":"./views/dashboard/dashboard.module#DashboardModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/dashboard/dashboard-routing.module.ts","module":"DashboardRoutingModule","children":[{"path":"","component":"DashboardComponent","data":{"title":"Dashboard"}}],"kind":"module"}],"module":"DashboardModule"}]},{"path":"icons","loadChildren":"./views/icons/icons.module#IconsModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/icons/icons-routing.module.ts","module":"IconsRoutingModule","children":[{"path":"","data":{"title":"Icons"},"children":[{"path":"coreui-icons","component":"CoreUIIconsComponent","data":{"title":"CoreUIIcons"}},{"path":"flags","component":"FlagsComponent","data":{"title":"Flags"}},{"path":"font-awesome","component":"FontAwesomeComponent","data":{"title":"FontAwesome"}},{"path":"simple-line-icons","component":"SimpleLineIconsComponent","data":{"title":"SimpleLineIcons"}}]}],"kind":"module"}],"module":"IconsModule"}]},{"path":"notifications","loadChildren":"./views/notifications/notifications.module#NotificationsModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/notifications/notifications-routing.module.ts","module":"NotificationsRoutingModule","children":[{"path":"","data":{"title":"Notifications"},"children":[{"path":"alerts","component":"AlertsComponent","data":{"title":"Alerts"}},{"path":"badges","component":"BadgesComponent","data":{"title":"Badges"}},{"path":"modals","component":"ModalsComponent","data":{"title":"Modals"}}]}],"kind":"module"}],"module":"NotificationsModule"}]},{"path":"theme","loadChildren":"./views/theme/theme.module#ThemeModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/theme/theme-routing.module.ts","module":"ThemeRoutingModule","children":[{"path":"","data":{"title":"Theme"},"children":[{"path":"colors","component":"ColorsComponent","data":{"title":"Colors"}},{"path":"typography","component":"TypographyComponent","data":{"title":"Typography"}}]}],"kind":"module"}],"module":"ThemeModule"}]},{"path":"widgets","loadChildren":"./views/widgets/widgets.module#WidgetsModule","children":[{"kind":"module","children":[{"name":"routes","filename":"src/app/views/widgets/widgets-routing.module.ts","module":"WidgetsRoutingModule","children":[{"path":"","component":"WidgetsComponent","data":{"title":"Widgets"}}],"kind":"module"}],"module":"WidgetsModule"}]}]}],"kind":"module"}]}
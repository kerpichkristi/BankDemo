import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { TransactionService } from '../transactions/transactions.service';
import { AuthorizedUser, UserService } from '../users/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  authorizedUser: AuthorizedUser;

  supportLanguages = ['en', 'he']

  constructor(private userService: UserService, private translateService: TranslateService) {

    this.translateService.addLangs(this.supportLanguages);
    this.translateService.setDefaultLang('en');

    const browserlang = this.translateService.getBrowserLang();
    this.translateService.use(browserlang);

  }
    

  ngOnInit() {
    

    this.userService.authorizedUser$.subscribe((authorizedUser: AuthorizedUser) => {
      this.authorizedUser = authorizedUser;
    });
    if (localStorage.getItem('token')) {
      this.authorizedUser = {
        Email: this.userService.getAuthorizedUserEmail()
      };
    }
  }
  collapse() {
    this.isExpanded = false;
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  selectLang(lang: string) {
    this.translateService.use(lang);
  }
}

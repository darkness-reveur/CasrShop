import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  @Input() user: User;

  @Output() updatePersonalInformation = new EventEmitter();

  isEditerMode: boolean;

  constructor() { }

  ngOnInit(): void {
    console.dir('onInitProfile')
    /* 
    this.userService.getUser().subscribe(result => {
      if (!result) {
        this.router.navigateByUrl('/login');
      }else {
        this.user = result
      }
    },
      error => {
      }) */

    console.dir('onInitProfileEnd')
  }


  startEditing() {
    console.log("start edit")
    console.log(this.isEditerMode)
    this.isEditerMode = true;
    console.log(this.isEditerMode)
  }

  saveChanges() {
    console.log("saveChanges")
    this.updatePersonalInformation.emit();

    this.isEditerMode = false;
  }
}

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSliderModule } from '@angular/material/slider';

import { ArtistService } from './services/artist.service';
import { AlbumService } from './services/album.service';
import { TrackService } from './services/track.service';
import { PlayerService } from './services/player.service';

import { ArtistComponent } from './artist/artist.component';
import { TrackComponent } from './track/track.component';
import { ArtistsComponent } from './chart/artists/artists.component';
import { TracksComponent } from './chart/tracks/tracks.component'
import { ArtistProfileComponent } from './artist-profile/artist-profile.component';
import { AlbumComponent } from './album/album.component';
import { AlbumPageComponent } from './album-page/album-page.component';

import { environment } from '../environments/environment';
import { AngularFireModule } from 'angularfire2';

import { AngularFirestoreModule } from 'angularfire2/firestore';
import { AngularFireStorageModule } from 'angularfire2/storage';
import { UploadDialogComponent } from './track/upload-dialog/upload-dialog.component';
import { DropZoneDirective } from './drop-zone.directive';
import { PlayerComponent } from './player/player.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ArtistComponent,
    TrackComponent,
    ArtistsComponent,
    TracksComponent,
    ArtistProfileComponent,
    AlbumComponent,
    AlbumPageComponent,
    UploadDialogComponent,
    DropZoneDirective,
    PlayerComponent
  ],
  entryComponents: [
    UploadDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    MatIconModule,
    MatToolbarModule,
    MatMenuModule,
    MatListModule,
    MatTabsModule,
    MatSliderModule,
    MatButtonModule,
    MatGridListModule,
    FlexLayoutModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatDialogModule,
    BrowserModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFirestoreModule,
    AngularFireStorageModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'chart/artists/:page', component: ArtistsComponent },
      { path: 'chart/tracks/:page', component: TracksComponent },
      { path: 'artist/:name', component: ArtistProfileComponent },
      { path: 'album/:name', component: AlbumPageComponent },
      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [
    ArtistService,
    AlbumService,
    TrackService,
    PlayerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

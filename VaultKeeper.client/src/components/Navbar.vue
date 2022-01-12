<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-primary px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex align-items-center">
        <img
          alt="logo"
          src="../assets/img/KnightsIcon.jpg"
          height="40"
          class="rounded elevation-2"
        />
        <span class="mx-3 text-shadow lighten-25 text-uppercase"
          >Vault Keeper</span
        >
      </div>
    </router-link>
    <div class="collapse navbar-collapse" id="navbarText"></div>
    <span class="navbar-text pe-3">
      <button
        class="
          btn btn-outline-warning
          text-shadow
          selectable
          text-success
          lighten-30
          text-uppercase
          my-2 my-lg-0
        "
        @click="login"
        v-if="!user.isAuthenticated"
      >
        Login
      </button>
      <div class="dropdown my-2 my-lg-0" v-else>
        <div
          class="dropdown-toggle selectable"
          data-bs-toggle="dropdown"
          aria-expanded="false"
          id="authDropdown"
        >
          <img
            :src="user.picture"
            alt="user photo"
            height="40"
            class="rounded elevation-2"
          />
          <span class="mx-3 text-success lighten-30 text-shadow">{{
            user.name
          }}</span>
        </div>
        <div
          class="dropdown-menu p-0 list-group w-100"
          aria-labelledby="authDropdown"
        >
          <router-link
            :to="{ name: 'Account', params: { accountId: account.id } }"
          >
            <div class="list-group-item list-group-item-action hoverable">
              Manage Account
            </div>
          </router-link>
          <div
            class="list-group-item list-group-item-action hoverable text-info"
            @click="logout"
          >
            <i class="mdi mdi-logout"></i>
            Logout
          </div>
        </div>
      </div>
    </span>
    <button
      class="navbar-toggler dropstart"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, watchEffect } from 'vue'
import { useRoute, useRouter } from "vue-router"
import { logger } from "../utils/Logger"
import { Modal } from "bootstrap"
export default {
  setup() {

    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      CreateKeep() {
        Modal.getOrCreateInstance(document.getElementById("CreateKeep")).toggle();
      },
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.bottom-border {
  border-bottom: 2px solid var(--bs-secondary);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
.navbar-clear {
  background-color: none;
}
</style>

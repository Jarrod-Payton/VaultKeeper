<template>
  <div class="card elevation-2 bg-dark m-2 action" @click="VaultPage()">
    <div class="card-body p-3">
      <div class="col-12">
        <div class="vault-title text-shadow text-center">
          <b>
            {{ vault.name }}
          </b>
        </div>
      </div>
      <div class="col-12">
        <div
          class="
            card
            bg-danger
            p-2
            mt-2
            isPrivate
            text-center text-light text-shadow
          "
          v-if="vault.isPrivate"
        >
          Private
        </div>
        <div
          class="
            card
            bg-warning
            p-2
            mt-2
            isPrivate
            text-center text-light text-shadow
          "
          v-else
        >
          Public
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { useRouter } from "vue-router"
import Pop from "../utils/Pop";
export default {
  props: { vault: { type: Object, required: true } },
  setup(props) {
    const router = useRouter();
    return {
      router,
      VaultPage() {
        router.push({ name: 'Vault', params: { vaultId: props.vault.id } })
      }
    }
  },
}
</script>
<style scoped>
.vault-title {
  font-size: 1.8vh;
}
.vault-owner {
  font-size: 1.3vh;
}
.isPrivate {
  font-size: 1.2vh;
}
</style>
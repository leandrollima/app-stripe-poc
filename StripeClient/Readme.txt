For test (Stripe):
https://docs.stripe.com/testing

Debit card number (visa) - BRASIL: 4000000760000002
CVC: any number
Expiry Date: any future date


DESCRIÇÃO									NÚMERO				CÓDIGO DE ERRO	CÓDIGO DE PAGAMENTO RECUSADO
Pagamento recusado por motivo genérico		4000000000000002	card_declined	generic_decline
Pagamento recusado por fundos insuficientes	4000000000009995	card_declined	insufficient_funds
Pagamento recusado por perda de cartão		4000000000009987	card_declined	lost_card
Pagamento recusado por roubo de cartão		4000000000009979	card_declined	stolen_card
Pagamento recusado por vencimento de cartão	4000000000000069	expired_card	n/a


STRIPE WEBHOOKS
====================================================================================

Instalar Stripe CLI (tem pra Windows, Docker, ...)
https://docs.stripe.com/stripe-cli#install

Adicionando um Path ao Ambiente pelo PowerShell:
$Env:Path += ';C:\Stripe'

https://dashboard.stripe.com/webhooks/create?endpoint_location=local
Baixe a CLI e faça login com sua conta Stripe: stripe login
Encaminhe eventos ao seu webhook: stripe listen --forward-to https://localhost:7078/webhook
Acione eventos com a CLI: stripe trigger payment_intent.succeeded

stripe listen --events payment_intent.created,payment_intent.succeeded,checkout.session.completed,payment_intent.payment_failed --forward-to https://localhost:7078/webhook

Eventos registrados para o webhook:
checkout.session.completed

Supported events:

  account.application.deauthorized
  account.updated
  balance.available
  charge.captured
  charge.dispute.created
  charge.failed
  charge.refund.updated
  charge.refunded
  charge.succeeded
  checkout.session.async_payment_failed
  checkout.session.async_payment_succeeded
  checkout.session.completed
  customer.created
  customer.deleted
  customer.source.created
  customer.source.updated
  customer.subscription.created
  customer.subscription.deleted
  customer.subscription.updated
  customer.updated
  identity.verification_session.canceled
  identity.verification_session.created
  identity.verification_session.redacted
  invoice.created
  invoice.finalized
  invoice.paid
  invoice.payment_action_required
  invoice.payment_failed
  invoice.payment_succeeded
  invoice.updated
  issuing_authorization.request
  issuing_authorization.request.eu
  issuing_authorization.request.gb
  issuing_card.created
  issuing_card.created.eu
  issuing_card.created.gb
  issuing_cardholder.created
  issuing_cardholder.created.eu
  issuing_cardholder.created.gb
  payment_intent.amount_capturable_updated
  payment_intent.canceled
  payment_intent.created
  payment_intent.partially_funded
  payment_intent.payment_failed
  payment_intent.requires_action
  payment_intent.succeeded
  payment_link.created
  payment_link.updated
  payment_method.attached
  payment_method.detached
  payout.created
  payout.updated
  plan.created
  plan.deleted
  plan.updated
  price.created
  price.updated
  product.created
  product.deleted
  product.updated
  quote.accepted
  quote.canceled
  quote.created
  quote.finalized
  reporting.report_run.succeeded
  setup_intent.canceled
  setup_intent.created
  setup_intent.setup_failed
  setup_intent.succeeded
  subscription.payment_failed
  subscription.payment_succeeded
  subscription_schedule.canceled
  subscription_schedule.created
  subscription_schedule.released
  subscription_schedule.updated